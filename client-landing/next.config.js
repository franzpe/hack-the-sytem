const path = require("path");
const withImages = require("next-images");
const withPlugins = require("next-compose-plugins");

const nextConfiguration = {};

module.exports = withPlugins(
  [
    withImages({
      exclude: path.resolve(__dirname, "assets/images/svgs"),
      assetPrefix: "",
      webpack(config, options) {
        config.module.rules.push({
          test: /\.svg$/,
          issuer: {
            test: /\.(js|ts)x?$/,
          },
          use: [
            {
              loader: "@svgr/webpack",
              options: {
                svgoConfig: {
                  plugins: {
                    removeViewBox: false,
                  },
                },
              },
            },
          ],
        });

        config.module.rules.push({
          test: /\.(ogg|mp3|wav|m4v|webm|mpe?g)$/i,
          exclude: config.exclude,
          use: [
            {
              loader: require.resolve("url-loader"),
              options: {
                limit: config.inlineImageLimit,
                fallback: require.resolve("file-loader"),
                publicPath: `${config.assetPrefix}/_next/static/images/`,
                name: "[name]-[hash].[ext]",
                esModule: config.esModule || false,
              },
            },
          ],
        });
        return config;
      },
    }),
  ],
  nextConfiguration
);
