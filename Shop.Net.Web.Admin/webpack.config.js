const path = require('path');
const CopyWebpackPlugin = require('copy-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');

module.exports = {
    mode: 'development', // Switch to 'production' when you're ready to deploy
    entry: './src/js/app.js', // Main entry point for your JS
    output: {
        filename: 'app.bundle.js', // Output bundled JS file
        path: path.resolve(__dirname, 'wwwroot/js'), // Output directory
        publicPath: '/js/' // Public path for assets
    },
    module: {
        rules: [
            {
                test: /\.css$/, // For processing CSS files
                use: [
                    MiniCssExtractPlugin.loader, // Extract CSS into a separate file
                    'css-loader',
                    'postcss-loader' // Use PostCSS to process Tailwind CSS
                ],
            },
            {
                test: /\.(png|jpg|gif|svg)$/, // For handling images (if needed)
                type: 'asset/resource',
                generator: {
                    filename: 'images/[hash][ext][query]', // Save images in 'images' folder with hashed names
                }
            }
        ]
    },
    plugins: [
        new CopyWebpackPlugin({
            patterns: [

            ]
        }),
        new MiniCssExtractPlugin({
            filename: '../css/styles.css', // Output CSS to wwwroot/css directory
        }),
    ],
    devtool: 'source-map',
    watch: true
};
