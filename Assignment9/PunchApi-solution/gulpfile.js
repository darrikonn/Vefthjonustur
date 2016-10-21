const gulp = require('gulp'),
      jshint = require('gulp-jshint'),
      stylish = require('jshint-stylish'),
      spawn = require('child_process').spawn;
var node;

/*
 * $ gulp server
 * description: launch the server. If there's a server already running, kill it.
 */
gulp.task('server', function() {
  if (node) node.kill()
  node = spawn('node', ['src/index.js'], {stdio: 'inherit'})
  node.on('close', function(code) {
    if (code === 8) {
      gulp.log('Error detected, waiting for changes...');
    }
  });
});

gulp.task('jshint', function() {
  return gulp.src('src/**.js')
    .pipe(jshint())
    .pipe(jshint.reporter(stylish));
});

gulp.task('default', ['server'], function() {
  gulp.watch('src/**.js', ['jshint', 'server']);
});

// clean up if an error goes unhandled.
process.on('exit', function() {
  if (node) node.kill();
});
