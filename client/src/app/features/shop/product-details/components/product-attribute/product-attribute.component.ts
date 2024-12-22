import { CommonModule } from '@angular/common';
import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-product-attribute',
  styleUrls: ['./product-attribute.component.scss'],
  template: `
    <div class="product-attribute mt-10">
      <label class="font-medium font-size-0.875rem ">
        <span>
          {{ attribute.productAttribute.attributeName }}:
          <span class="ml-1 font-semibold">{{ selectedOption }}</span>
        </span>
      </label>
      <div class="grid grid-cols-5 gap-2 mt-3">
        <div
          *ngFor="let option of attribute.productAttribute.attributeOptions"
          class="relative rounded-2xl cursor-pointer"
          [class.active]="option.id === selectedOptionId"
          (click)="selectOption(option)"
        >
          <span class="option-text">{{ option.optionText }}</span>
        </div>
      </div>
    </div>
  `,
  standalone: true,
  imports: [CommonModule],
})
export class ProductAttributeComponent {
  @Input() attribute!: {
    productAttribute: {
      attributeOptions: { id: number; optionText: string }[];
      attributeName: string;
    };
  };

  @Output() optionSelected = new EventEmitter<number>();

  selectedOptionId: number | null = null;
  selectedOption: string = '';

  selectOption(option: { id: number; optionText: string }) {
    this.selectedOptionId = option.id;
    this.selectedOption = option.optionText;
    this.optionSelected.emit(option.id);
  }
}
