export default class ArrayHelper {
    static intersect = <T>(x: T[], y: T[]): T[] => {
        const set = new Set(y);
        return x.filter(i => set.has(i));
    };
    
    static except = <T>(x: T[], y: T[]): T[] => {
        const set = new Set(y);
        return x.filter(i => !set.has(i));
    };
    
    static isIntersects = <T>(x: T[], y: T[]): boolean => {
        return ArrayHelper.intersect(x,  y).length > 0;
    };
}
