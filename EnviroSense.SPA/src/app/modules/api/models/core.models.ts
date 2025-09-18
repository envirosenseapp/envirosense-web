export interface BaseFilters{
  pageSize: number;
  pageIndex :number;
}

export interface PagedApiResult<T> {
  records: T[];
  pageSize: number;
  pageIndex :number;
  totalCount: number;
  totalPages: number;
}

export interface ErrorResponse {
   code: string;
   message: string;
   context: ErrorResponseEntry[] | null;
}

export interface ErrorResponseEntry {
    key: string;
    message: string;
}

export function EmptyPagedApiResult<T>(): PagedApiResult<T> {
  return {
    records: [],
    pageSize: 20,
    pageIndex: 0,
    totalCount: 0,
    totalPages: 0,
  }
}
