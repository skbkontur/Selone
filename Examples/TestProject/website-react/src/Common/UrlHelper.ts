import * as querystring from "querystring";

export default class UrlHelper {
    static buildUrl(path: string, params: {}) {
        return `${path}?${querystring.stringify(params)}`;
    }
}

