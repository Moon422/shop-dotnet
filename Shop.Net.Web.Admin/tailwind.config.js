/** @type {import('tailwindcss').Config} */
module.exports = {
  purge: ['./Views/**/*.cshtml', './Scripts/**/*.js'],
  darkMode: false,
  content: ['./Views/**/*.cshtml', './src/**/*.{js,css}'],
  theme: {
    extend: {},
  },
  plugins: [],
}

