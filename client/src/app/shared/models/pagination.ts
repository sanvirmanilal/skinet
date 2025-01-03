export type Pagination<T> = {
    pageSize: number;
    pageIndex: number;
    count: number;
    items: T[]
}