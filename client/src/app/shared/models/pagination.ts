export type ApiResponse<T> = {
  statusCode: number;
  message: string;
  data: T;
};

export type Pagination<T> = {
  pageIndex: number;
  pageSize: number;
  count: number;
  data: T[];
};
