const path = require('path');
const CopyWebpackPlugin = require('copy-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const TerserPlugin = require('terser-webpack-plugin');

module.exports = {
    mode: 'development',
    entry: './src/js/app.js',
    output: {
        filename: 'app.bundle.js',
        path: path.resolve(__dirname, 'wwwroot/js'),
        publicPath: '/js/'
    },
    module: {
        rules: [
            {
                test: /\.css$/,
                use: [
                    MiniCssExtractPlugin.loader,
                    'css-loader',
                    'postcss-loader'
                ],
            },
            {
                test: /\.(png|jpg|gif|svg)$/,
                type: 'asset/resource',
                generator: {
                    filename: 'images/[hash][ext][query]',
                }
            }
        ]
    },
    plugins: [
        new CopyWebpackPlugin({
            patterns: [
                { from: './node_modules/jquery/dist', to: path.resolve(__dirname, 'wwwroot/lib/jquery') },
            ]
        }),
        new MiniCssExtractPlugin({
            filename: '../css/styles.css',
        }),
    ],
    optimization: {
        minimize: true,
        minimizer: [
            new TerserPlugin({
                terserOptions: {
                    compress: {
                        drop_console: true,
                    },
                },
            }),
            // new CssMinimizerPlugin()
        ],
    },
    devtool: 'source-map',
    watch: true
};
