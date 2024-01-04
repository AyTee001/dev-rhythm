const path = require("path");
const glob = require("glob");

module.exports = {
    entry: {
        layout: getPathsToFiles("./wwwroot/ts-scripts/Layout/*.ts"),
        notification: getPathsToFiles("./wwwroot/ts-scripts/Notification/*.ts"),
        post: getPathsToFiles("./wwwroot/ts-scripts/Post/*.ts")
    },
    output: {
        path: path.resolve(__dirname, "wwwroot/js/"),
        filename: "[name].bundle.js",
        publicPath: "/",
    },
    resolve: {
        extensions: [".js", ".ts"],
    },
    module: {
        rules: [
            {
                test: /\.ts$/,
                use: "ts-loader",
            }
        ],
    }
};


function getPathsToFiles(folderpath) {
    return glob.sync(folderpath).map(filePath => `./${filePath}`);
}