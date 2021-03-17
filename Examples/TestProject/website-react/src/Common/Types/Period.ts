import ObjectHelper from "src/Common/TypeHelpers/ObjectHelper";

export default class Period {
    constructor(from?: Date, to?: Date) {
        this.from = from || null;
        this.to = to || null;
    }

    from: Date;
    to: Date;

    equals(other: Period) {
        return Period.equals(this, other);
    }

    static equals(x: Period, y: Period) {
        return ObjectHelper.isEqual(x, y);
    }
}