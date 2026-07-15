import * as React from "react";
import type {Preview} from "@storybook/react";
import {LIGHT_THEME, ThemeContext, ThemeFactory} from "@skbkontur/react-ui";

const theme = ThemeFactory.create(
    {
        btnDefaultBorderColor: "#b3b3b3",
        borderColorDisabled: "#b3b3b3",
    },
    LIGHT_THEME,
);

const preview: Preview = {
    decorators: [
        Story => (
            <ThemeContext.Provider value={theme}>
                <div className={"react-ui"} style={{padding: 20}}>
                    <Story/>
                </div>
            </ThemeContext.Provider>
        ),
    ],
};

export default preview;
