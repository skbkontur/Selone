import * as moment from "moment";

moment.locale("ru");

export default class DateHelper {
    static momentFormat = (date: Date, format?: string): string => {
        return moment(date).format(format || "L");
    };
    
    static addDays = (date: Date, days: number): Date => {
        return moment(date).add(days, "days").toDate();
    };
}
