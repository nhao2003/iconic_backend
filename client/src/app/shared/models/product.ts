export type Product = {
  id: number;
  name: string;
  description: string;
  price: number;
  discountedPrice?: number;
  imageUrl: string;
  type: string;
  brand: string;
  quantityInStock: number;
  visibility: boolean;
  sku: string;
  barcode: string;
  weight: number;
  category: {
    id: number;
    slug: string;
    status: boolean;
    includeInNav: boolean;
    position: number;
    categoryDescriptions: {
      id: number;
      name: string;
      shortDescription: string;
      description: string;
      image: string;
      urlKey: string;
    }[];
  };
  productAttributes: {
    productAttribute: {
      id: number;
      attributeCode: string;
      attributeName: string;
      type: string;
      isRequired: boolean;
      displayOnFrontend: boolean;
      sortOrder: number;
      isFilterable: boolean;
      attributeOptions: any[];
    };
  }[];
  variants: {
    id: number;
    sku: string;
    price: number;
    imageUrl: string;
    attributeValues: {
      id: number;
      optionText: string;
      variantId: number;
      attributeId: number;
      option: any;
    }[];
  }[];
  productCustomOptions: {
    id: number;
    optionName: string;
    optionType: string;
    isRequired: boolean;
    sortOrder: number;
    productCustomOptionValues: {
      id: number;
      extraPrice: number;
      sortOrder: number;
      value: string;
    }[];
  }[];
};
