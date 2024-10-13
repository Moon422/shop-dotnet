/** @type {import('tailwindcss').Config} */

const daisyui = require('daisyui');

module.exports = {
  purge: ['./Views/**/*.cshtml', './Scripts/**/*.js'],
  darkMode: false,
  content: ['./Views/**/*.cshtml', './src/**/*.{js,css}'],
  theme: {
    extend: {},
  },
  plugins: [
    daisyui,
  ],
}

