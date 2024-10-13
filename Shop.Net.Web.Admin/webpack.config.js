const path = require('path');

module.exports = {
    mode: 'development', // Switch to 'production' when you're ready to deploy
    entry: './src/app.js', // Main entry point for your JS
    output: {
        filename: 'app.bundle.js', // Output bundled JS file
        path: path.resolve(__dirname, 'wwwroot/js'), // Output directory
        publicPath: '/js/' // Public path for assets
    },
    module: {
        rules: [
            {
                test: /\.css$/, // For processing CSS files
                use: ['style-loader', 'css-loader', 'postcss-loader'], // Use PostCSS for Tailwind
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
    devtool: 'source-map', // Optional: for easier debugging in development
};
