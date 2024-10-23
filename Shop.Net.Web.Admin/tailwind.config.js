/** @type {import('tailwindcss').Config} */

const daisyui = require('daisyui');

module.exports = {
  purge: ['./Views/**/*.cshtml', './Scripts/**/*.js'],
  darkMode: false,
  content: ['./Views/**/*.cshtml', './src/**/*.{js,css}'],
  theme: {
    extend: {},
  },
  safelist: ['join', 'join-item', 'btn', 'btn-active', 'text-center'],
  plugins: [
    daisyui,
  ],
  daisyui: {
    themes: ["light", "dark"],
  },
}
