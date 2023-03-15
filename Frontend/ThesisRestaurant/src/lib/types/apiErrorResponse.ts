export interface ApiErrorResponse {
    [key: string]: string | number | {};
    type: string;
    title: string;
    status: number;
    traceId: string;
    erorrs:{
        [key: string]: string[]
    }
}
