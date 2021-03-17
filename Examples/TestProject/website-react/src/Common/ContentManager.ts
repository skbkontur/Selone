export default class ContentManager {
    static downloadFile = (url: string): void => {
        window.location.href = url;
    }
}