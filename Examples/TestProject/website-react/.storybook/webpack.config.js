const path = require("path");
const webpack = require("webpack");

module.exports = function (baseConfig, env, defaultConfig) {
    return {
        ...defaultConfig,
        module: {
            ...defaultConfig.module,
            rules: [
                {
                    test: /\.(js|jsx)$/,
                    include: /(retail-ui|config\.js)/,
                    loader: "babel-loader"

                },
                {
                    test: /\.(ts|tsx)$/,
                    exclude: /node_modules/,
                    loader: "ts-loader"

                },
                {
                    test: /\.css$/,
                    loader: [
                        "style-loader",
                        {
                            loader: "css-loader",
                            /*options: {
                                modules: false,
                            },*/
                        },

                    ]
                },
                {
                    test: /\.less$/,
                    loaders: [
                        "style-loader",
                        {
                            loader: "css-loader",
                            /*options: {
                                modules: false,
                            },*/
                        },
                        "less-loader",
                    ],
                },
                {
                    test: /\.(woff|woff2|eot|png|gif|ttf|jpg|svg)$/,
                    loader: "file-loader"
                },
            ]
        },
        resolve: {
            ...defaultConfig.resolve,
            extensions: [".js", ".jsx", ".ts", ".tsx"],
            alias: {
                ...defaultConfig.resolve.alias,
                src: path.resolve(__dirname, "../src"),
            },
        },
        plugins: [
            ...defaultConfig.plugins.filter(x => x.constructor.name !== "CaseSensitivePathsPlugin"),
            new webpack.DefinePlugin({
                "process.env.enableReactTesting": JSON.stringify(true),
            }),
        ]
    };
}
;