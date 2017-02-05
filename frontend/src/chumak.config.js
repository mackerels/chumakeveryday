const path = require('path');
const webpack = require('webpack');
const ForkCheckerPlugin = require('awesome-typescript-loader').CheckerPlugin;
const HtmlWebpackPlugin = require('html-webpack-plugin');
const CleanWebpackPlugin = require('clean-webpack-plugin');
const UglifyJsPlugin = require('webpack-uglify-js-plugin');

function getRoot(args) {
  const _root = path.resolve(__dirname, '..');
  args = Array.prototype.slice.call(arguments, 0);

  return path.join.apply(path, [_root].concat(args));
}

module.exports = {
  entry: {
    'js/chumak': './src/index.ts'
  },
  output: {
    path: getRoot("../wwwroot"),
    filename: '[name].[hash].bundle.js',
    sourceMapFilename: '[name].[hash].bundle.map'
  },
  resolve: {
    extensions: ['', '.ts', '.js'],
    modulesDirectories: ['node_modules'],
    root: [getRoot('src')]
  },
  resolveLoader: {
    modulesDirectories: [
      getRoot('./node_modules')
    ]
  },
  module: {
    unknownContextRegExp: /$^/,
    unknownContextCritical: false,
    exprContextRegExp: /$^/,
    exprContextCritical: false,
    wrappedContextCritical: true,
    preLoaders: [
      /*
       * Source maps
       */
      {
        test: /\.js$/,
        loader: 'source-map-loader'
      }
    ],
    loaders: [
      /*
       * Typescript loader
       */
      {
        test: /\.ts$/,
        loader: 'awesome-typescript-loader'
      },
      {
        test: /\.json$/,
        loader: 'json-loader'
      },
      {
        test: /\.md$/,
        loader: "html!markdown"
      },
      {
        test: /\.css$/,
        loader: "style-loader!css-loader"
      }
    ]
  },
  plugins: [
    /*
     * Plugin: ForkCheckerPlugin
     * Description: Do type checking in a separate process, so webpack don't need to wait
     */
    new ForkCheckerPlugin(),
    /*
     * Plugin: OccurenceOrderPlugin
     * Description: Varies the distribution of the ids to get the smallest id length
     * for often used ids.
     */
    new webpack.optimize.OccurenceOrderPlugin(true),

    new HtmlWebpackPlugin({
      template: 'src/templates/chumak.template.html',
      filename: 'index.html',
      chunks: ['js/chumak'],
      minify: {
        removeAttributeQuotes: true,
        removeComments: true,
        collapseWhitespace: true
      }
    }),
    new CleanWebpackPlugin(['../wwwroot'], {
      root: getRoot('src'),
      verbose: true,
      dry: false
    }),
    new UglifyJsPlugin({
      cacheFolder: getRoot('node_modules/webpack-uglify-js-plugin/cache'),
      debug: true,
      minimize: true,
      sourceMap: true,
      output: {
        comments: false
      },
      compressor: {
        warnings: false
      }
    }),
    new webpack.ProvidePlugin({
      jQuery: 'jquery',
      $: 'jquery',
      jquery: 'jquery',
      bootstrap: 'bootstrap'
    })
  ],
  debug: true,
  watchOptions: {
    poll: false,
    ignored: './node_modules/'
  },
  node: {
    global: 'window',
    crypto: 'empty',
    process: true,
    module: false,
    clearImmediate: false,
    setImmediate: false
  }
};
