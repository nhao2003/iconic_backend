export interface DescriptionItem {
  type: 'text' | 'image';
  text?: string;
  image?: {
    height: number;
    width: number;
    uri: string;
    url_list: string[];
  };
}
