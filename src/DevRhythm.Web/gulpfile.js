/// <binding ProjectOpened='sass-to-css:watch, watch-ts-scripts' />
'use strict';
const gulp = require('gulp'),
    sass = require('gulp-sass')(require('sass')),
    concat = require('gulp-concat'),
    plumber = require('gulp-plumber'),
    webpackStream = require('webpack-stream'),
    webpackConfig = require('./webpack.config.js');

const scssPath = './wwwroot/scss/**/*.scss';
const tsPath = './wwwroot/ts-scripts/**/*.ts';

gulp.task('sass-to-css', async function () {
    return gulp.src(scssPath)
        .pipe(plumber())
        .pipe(sass().on('error', sass.logError))
        .pipe(concat('styles.css'))
        .pipe(gulp.dest('./wwwroot/css'));
});

gulp.task('sass-to-css:watch', function () {
    gulp.watch(scssPath, gulp.series('sass-to-css'));
});

gulp.task('webpack', function () {
    return gulp.src(tsPath)
        .pipe(webpackStream(webpackConfig))
        .pipe(gulp.dest(webpackConfig.output.path));
});

gulp.task('webpack:watch', function () {
    gulp.watch(tsPath, gulp.series('webpack'));
});