var path = require("path");

module.exports = {
  mode: "development",
  entry: "./src/Startup/Startup.fs.js",
  output: {
    path: path.join(__dirname, "./public"),
    filename: "bundle.js",
  },
  devServer: {
    static: {
      directory: path.join(__dirname, "public"),
    },
    compress: true,
    port: 8080,
  },
};