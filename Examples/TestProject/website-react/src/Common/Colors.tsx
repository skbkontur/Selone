export enum GrayColor {
    ContentBackground,
    BlockBackground,
    ServiceBackground,
    SelectedItem,
    WatermarkText,
    DisabledText,
    AdditionalText,
    Text,
    Black,
}

export const GrayColors = {
    [GrayColor.ContentBackground]: "#FFFFFF",
    [GrayColor.BlockBackground]: "#F2F2F2",
    [GrayColor.ServiceBackground]: "#E5E5E5",
    [GrayColor.SelectedItem]: "#E5E5E5",
    [GrayColor.WatermarkText]: "#A0A0A0",
    [GrayColor.DisabledText]: "#A0A0A0",
    [GrayColor.AdditionalText]: "#808080",
    [GrayColor.Text]: "#333333",
    [GrayColor.Black]: "#000000",
};

export enum AccentColor {
    Green,
    Red,
    Gray,
    LightGray,
    Orange,
    Blue,
}

export enum AccentColorType {
    Background,
    Clickable,
    Text,
}

export const AccentColors = {
    [AccentColor.Green]: {
        [AccentColorType.Background]: "#E2F7DC",
        [AccentColorType.Clickable]: "#3F9726",
        [AccentColorType.Text]: "#228007",
    },
    [AccentColor.Red]: {
        [AccentColorType.Background]: "#FFD6D6",
        [AccentColorType.Clickable]: "#D70C17",
        [AccentColorType.Text]: "#CE0014",
    },
    [AccentColor.Gray]: {
        [AccentColorType.Background]: "#E5E5E5",
        [AccentColorType.Clickable]: "#808080",
        [AccentColorType.Text]: "#808080",
    },
    [AccentColor.LightGray]: {
        [AccentColorType.Background]: "#E5E5E5",
        [AccentColorType.Clickable]: "#CCCCCC",
        [AccentColorType.Text]: "#CCCCCC",
    },
    [AccentColor.Orange]: {
        [AccentColorType.Background]: "#FFF0BC",
        [AccentColorType.Clickable]: "#F69C00",
        [AccentColorType.Text]: "#D97E00",
    },
    [AccentColor.Blue]: {
        [AccentColorType.Background]: "#E4F3FF",
        [AccentColorType.Clickable]: "#5199DB",
        [AccentColorType.Text]: "#3072C4",
    },
};

export enum ProductColor {
    Background,
    Light,
    Branded,
    Text,
    MenuSelected,
}

export const ProductColors = {
    [ProductColor.Background]: "#F5E0F3",
    [ProductColor.Light]: "#B254AA",
    [ProductColor.Branded]: "#A23A99",
    [ProductColor.Text]: "#902987",
    [ProductColor.MenuSelected]: "#7A1871",
};