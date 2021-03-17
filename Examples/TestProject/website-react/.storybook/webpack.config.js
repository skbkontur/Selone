const path = require('path');
const webpack = require("webpack");
const genDefaultConfig = require('@storybook/react/dist/server/config/defaults/webpack.config.js');

module.exports = function (config, env) {
    config = genDefaultConfig(config, env);

    config.module.rules.push({
        test: /\.(js|jsx)$/,
        include: /(retail-ui|config\.js)/,
        loader: "babel-loader"
    });

    config.module.rules.push({
        test: /\.(ts|tsx)$/,
        exclude: /node_modules/,
        loader: "ts-loader"
    });

    config.module.rules.push({
        test: /\.css$/,
        loader: "css-loader"
    });

    config.module.rules.push({
        test: /\.less$/,
        loaders: ["style-loader", "css-loader", "less-loader"]
    });

    config.module.rules.push({
        test: /\.svg$/,
        exclude: /node_modules/,
        loader: "svg-react-loader"
    });

    config.resolve.extensions.push(".tsx");
    config.resolve.extensions.push(".ts");
    config.resolve.extensions.push(".jsx");
    config.resolve.extensions.push(".js");
    config.resolve.extensions.push(".css");
    config.resolve.extensions.push(".less");
    config.resolve.extensions.push(".svg");

    config.resolve.alias["react-ui-theme"] = path.resolve(__dirname, "../src/react-ui-theme.less");
    config.resolve.alias["src"] = path.resolve(__dirname, "../src");

    config.plugins.push(new webpack.DefinePlugin({
        "process.env.enableReactTesting": JSON.stringify(true),
    }));

    return config;
};