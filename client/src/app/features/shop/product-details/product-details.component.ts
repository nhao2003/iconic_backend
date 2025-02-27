import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../../core/services/shop.service';
import { ActivatedRoute } from '@angular/router';
import { Product } from '../../../shared/models/product';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { MatDivider } from '@angular/material/divider';
import { CartService } from '../../../core/services/cart.service';
import { FormsModule } from '@angular/forms';
import { environment } from '../../../../environments/environment';
import { AppBagIconComponent } from './components/app-bag-icon/app-bag-icon.component';
import { AppButtonPrimaryComponent } from './components/app-button-primary/app-button-primary.component';
import { AppNcInputNumberComponent } from './components/app-nc-input-number/app-nc-input-number.component';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { DescriptionItem } from '../../../shared/models/descriptionItem';
import { ProductAttributeComponent } from './components/product-attribute/product-attribute.component';

@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [
    CurrencyPipe,
    MatButton,
    MatIcon,
    MatFormField,
    MatInput,
    MatLabel,
    MatDivider,
    FormsModule,
    CommonModule,
    AppBagIconComponent,
    AppButtonPrimaryComponent,
    AppNcInputNumberComponent,
    ProductAttributeComponent,
  ],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss',
})
export class ProductDetailsComponent implements OnInit {
  private shopService = inject(ShopService);
  private activatedRoute = inject(ActivatedRoute);
  private cartService = inject(CartService);
  product?: Product;
  quantityInCart = 0;
  quantity = 1;
  baseUrl = environment.baseUrl;
  selectedImage: string = '';
  isImageModalOpen = false;
  formattedDescription: DescriptionItem[] = [];
  selectedAttributes: { [key: string]: number } = {};

  constructor(private sanitizer: DomSanitizer) {}

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (!id) return;
    this.shopService.getProduct(+id).subscribe({
      next: (product) => {
        this.product = product.data;

        if (this.product) {
          // Đặt ảnh chính ban đầu
          this.selectedImage = this.product.imageUrl;

          // Thêm ảnh chính vào danh sách `imageCoverUrls`
          this.product.imageCoverUrls = [
            this.product.imageUrl,
            ...this.product.imageCoverUrls,
          ];

          try {
            const parsedDescription = JSON.parse(this.product.description);
            if (Array.isArray(parsedDescription)) {
              this.formattedDescription =
                parsedDescription as DescriptionItem[];
            }
          } catch {
            console.error('Lỗi khi parse description JSON');
          }

          this.filterAttributeOptions();
        }

        this.updateQuantityInCart();
      },
      error: (error) => console.log(error),
    });
  }

  filterAttributeOptions() {
    if (!this.product) return;

    // Tạo một danh sách các attributeOptionId có trong các variant
    const variantOptionIds = this.product.variants.flatMap((variant) =>
      variant.attributeValues.map((attributeValue) => attributeValue.option.id)
    );

    console.log('Variant Option IDs:', variantOptionIds); // Log các ID của các option có trong các variant

    // Lọc các attributeOptions của mỗi productAttribute
    this.product.productAttributes.forEach((attribute) => {
      console.log(
        'Before Filtering Attribute Options:',
        attribute.productAttribute.attributeOptions
      ); // Log trước khi lọc

      attribute.productAttribute.attributeOptions =
        attribute.productAttribute.attributeOptions.filter((option) =>
          variantOptionIds.includes(option.id)
        );

      console.log(
        'After Filtering Attribute Options:',
        attribute.productAttribute.attributeOptions
      ); // Log sau khi lọc
    });
  }

  formatTextWithNewLine(text: string | undefined): string {
    if (!text) return '';
    return text.replace(/\n/g, '<br>');
  }

  changeMainImage(image: string) {
    this.selectedImage = image;
  }

  openImageModal() {
    this.isImageModalOpen = true;
  }

  closeImageModal() {
    this.isImageModalOpen = false;
  }

  updateCart() {
    if (!this.product) return;
    if (this.quantity > this.quantityInCart) {
      const itemsToAdd = this.quantity - this.quantityInCart;
      this.quantityInCart += itemsToAdd;
      this.cartService.addItemToCart(this.product, itemsToAdd);
    } else {
      const itemsToRemove = this.quantityInCart - this.quantity;
      this.quantityInCart -= itemsToRemove;
      this.cartService.removeItemFromCart(this.product.id, itemsToRemove);
    }
  }

  updateQuantityInCart() {
    this.quantityInCart =
      this.cartService
        .cart()
        ?.items.find((x) => x.productId === this.product?.id)?.quantity || 0;
    this.quantity = this.quantityInCart || 1;
  }

  getButtonText() {
    return this.quantityInCart > 0 ? 'Update cart' : 'Add to cart';
  }

  onQuantityChange(newQuantity: number) {
    this.quantity = newQuantity;
  }

  sanitizeUrl(url: string): SafeUrl {
    return this.sanitizer.bypassSecurityTrustUrl(url);
  }

  onAttributeOptionSelected(attributeCode: string, optionId: number) {
    this.selectedAttributes[attributeCode] = optionId;
    console.log('Selected Attributes:', this.selectedAttributes);
  }

  // filterAttributeOptions(attributeId: number) {
  //   // Kiểm tra xem sản phẩm có các variants hay không
  //   console.log('Product Variants:', this.product?.variants);
  //   if (!this.product?.variants) return [];

  //   // Tạo danh sách các attributeValues thuộc Variant
  //   const variantAttributeValues = this.product.variants.flatMap((variant) => {
  //     const filteredValues = variant.attributeValues.filter(
  //       (value) => value.attributeId === attributeId
  //     );
  //     console.log(
  //       'Filtered attributeValues for variant',
  //       variant.id,
  //       ':',
  //       filteredValues
  //     );
  //     return filteredValues;
  //   });

  //   console.log(
  //     'All filtered variant attributeValues:',
  //     variantAttributeValues
  //   );

  //   // Trả về các attributeOptions có trong attributeValues của variants
  //   const attributeOptions =
  //     this.product?.productAttributes
  //       .find((attr) => attr.productAttribute.id === attributeId)
  //       ?.productAttribute.attributeOptions.filter((option) =>
  //         variantAttributeValues.some((value) => value.option.id === option.id)
  //       ) || [];

  //   console.log('Filtered attributeOptions:', attributeOptions);

  //   return attributeOptions;
  // }
}
