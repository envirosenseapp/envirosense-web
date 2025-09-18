export class Empty {}

export type Constructor<T = {}> = new (...args: any[]) => T;