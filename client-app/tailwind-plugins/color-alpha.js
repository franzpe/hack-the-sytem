const plugin = require('tailwindcss/plugin');
const Color = require('color');

/**
 * Generates colors with alpha channel by opacity values
 *
 * ex: bg-primary-2-navy-alpha-50
 */
module.exports = plugin(function ({ addUtilities, theme, variants }) {
  const PREFIXES = {
    backgroundColor: ['bg'],
    borderColor: ['border', 'border-t', 'border-r', 'border-b', 'border-l'],
    fill: ['fill'],
    stroke: ['stroke'],
    textColor: ['text']
  };

  const PROPERTIES = {
    backgroundColor: ['backgroundColor'],
    borderColor: ['borderColor', 'borderTopColor', 'borderRightColor', 'borderBottomColor', 'borderLeftColor'],
    fill: ['fill'],
    stroke: ['stroke'],
    textColor: ['color']
  };

  let colors = theme('colors', []);
  let opacities = theme('opacity', []);

  for (const [key, value] of Object.entries(colors)) {
    const colorGroup = typeof value === 'string' ? { [key]: value } : value;

    if (
      typeof value === 'object' ||
      value.startsWith('var') ||
      value.startsWith('current') ||
      value.startsWith('transparent')
    ) {
      continue;
    }

    for (const [colorName, colorValue] of Object.entries(colorGroup)) {
      for (const o in opacities) {
        const colorVariant = typeof value === 'string' || colorName === 'default' ? key : `${key}-${colorName}`;

        for (const [variant, properties] of Object.entries(PREFIXES)) {
          const newColors = {};
          properties.forEach((property, index) => {
            {
              newColors[`.${property}-${colorVariant}-alpha-${o}`] = {
                [`${PROPERTIES[variant][index]}`]: Color(colorValue).alpha(opacities[o]).string()
              };
            }
          });
          addUtilities(newColors, variants(variant));
        }
      }
    }
  }
});
