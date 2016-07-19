module.exports = function (grunt) {
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),

        bundler: {
            bundle: {
                views: ['index.html'],
                bundles: {
                    //'css': {
                    //    type: 'css',
                    //    files: ['css/*.css']
                    //},
                    'js': {
                        type: 'js',
                        files: ['app/**/*.js']
                    }
                }
            }
        }
    });

    grunt.loadNpmTasks('grunt-bundler');

    grunt.registerTask('default', ['bundler']);

};
