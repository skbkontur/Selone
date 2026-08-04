import path from "path";
import type {StorybookConfig} from "@storybook/react-webpack5";

const config: StorybookConfig = {
    stories: ["../stories/**/*.stories.@(ts|tsx)"],
    addons: ["@storybook/addon-webpack5-compiler-babel"],
    framework: {
        name: "@storybook/react-webpack5",
        options: {},
    },
    webpackFinal: async baseConfig => {
        return {
            ...baseConfig,
            entry: [
                require.resolve("./testing-setup.js"),
                ...(Array.isArray(baseConfig.entry) ? baseConfig.entry : []),
            ],
            module: {
                ...baseConfig.module,
                rules: [
                    ...(baseConfig.module?.rules ?? []),
                    {
                        test: /\.(ts|tsx)$/,
                        exclude: /node_modules/,
                        loader: "ts-loader",
                    },
                    {
                        test: /\.less$/,
                        use: [
                            "style-loader",
                            {
                                loader: "css-loader",
                                options: {
                                    modules: {
                                        mode: "local",
                                    },
                                },
                            },
                            "less-loader",
                        ],
                    },
                    {
                        test: /\.(woff|woff2|eot|png|gif|ttf|jpg|svg)$/,
                        type: "asset/resource",
                    },
                ],
            },
            resolve: {
                ...baseConfig.resolve,
                extensions: [...(baseConfig.resolve?.extensions ?? []), ".ts", ".tsx"],
                alias: {
                    ...baseConfig.resolve?.alias,
                    src: path.resolve(__dirname, "../src"),
                },
            },
        };
    },
};

export default config;
