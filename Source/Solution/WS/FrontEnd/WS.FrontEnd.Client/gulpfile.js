var gulp = require('gulp');
var gls = require('gulp-live-server');
var concat = require('gulp-concat');
var clean = require('gulp-clean');
var pipemin = require('gulp-pipemin');
var flatten = require('gulp-flatten');
var cssimport = require("gulp-cssimport");
var uglify = require('gulp-uglify');
var uglifycss = require('gulp-uglifycss');
var templateCache = require('gulp-angular-templatecache');
var htmlmin = require('gulp-htmlmin');

var paths = {
    root: "./wwwroot/",
    app: "app/",
    lib: "lib/",
    dist: "../dist/"
}

gulp.task('serve-dist', ["build"], function () {
    var server = gls.static(paths.dist, 80);
    return server.start();
});

gulp.task('build', ["clean", "templates", "copy-fonts", "pipe-min"], function () {
    return gulp.src([paths.dist + "js/app.js", paths.dist + "js/templates.js"])
    .pipe(concat('app.js'))
    .pipe(gulp.dest(paths.dist + "js"));
});

gulp.task("pipe-min", ["clean", "build-template-cache", "copy-fonts"], function () {
    return gulp.src(paths.root + "index.html")
    .pipe(pipemin({
        css: function (stream, concat) {
            return stream
                .pipe(cssimport())
                .pipe(uglifycss({
                    "maxLineLen": 80,
                    "uglyComments": true
                }))
                .pipe(concat);
        },
        js: function (stream, concat) {
            return stream
                .pipe(uglify())
                .pipe(count())
                .pipe(concat);
        },
        js1: function (stream, concat) {
            return stream
                .pipe(uglify())
                .pipe(concat);
        }
    }))
   .pipe(gulp.dest(paths.dist));
});

gulp.task("build-template-cache", ["clean"], function () {
    return gulp
        .src([paths.root + "app/**/*.html"])
        .pipe(htmlmin({ collapseWhitespace: true }))
        .pipe(templateCache("templates.js", {
            root: "app",
            module: 'ws.core',
            standAlone: false
        }))
        .pipe(gulp.dest(paths.dist + "js"));
});

gulp.task("clean", function () {
    return gulp.src(paths.dist, { read: false }).pipe(clean({ force: true }));
});

gulp.task("copy-fonts", ["clean"], function () {
    return gulp.src(paths.root + paths.lib + "**/*.{ttf,woff,woff2,eof,svg}")
    .pipe(flatten())
   .pipe(gulp.dest(paths.dist + "css/fonts"));
});

gulp.task('serve-dev', function () {
    // place code for your default task here
    var server = gls.static("wwwroot", 80); //equals to gls.static('public', 3000); 
    server.start();

    gulp.watch(
       [
           paths.root + "app/**/*.html",
           paths.root + "**/*.css",
           paths.root + "**/*.js",
           paths.root + "index.html"
       ],
       function (file) {
           server.notify(file);
       });
});

gulp.task('default', ["dev-serve"], function () {
    // place code for your default task here
});