/// <binding ProjectOpened='sass-to-css:watch, watch-ts-scripts' />
"use strict";
const gulp = require("gulp"),
    sass = require('gulp-sass')(require('sass')),
    concat = require('gulp-concat'),
    ts = require('gulp-typescript'),
    plumber = require('gulp-plumber');

const tsProject = ts.createProject('tsconfig.json');
const tsPath = './wwwroot/ts-scripts/**/*.ts';
const scssPath = './wwwroot/scss/**/*.scss';

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

gulp.task('ts-compile', function () {
    const tsResult = gulp.src(tsPath).pipe(tsProject());

    return tsResult.js
        .pipe(plumber())
        .pipe(gulp.dest('./wwwroot/js'));
});

gulp.task('watch-ts-scripts', function () {
    gulp.watch(tsPath, gulp.series('ts-compile'));
});