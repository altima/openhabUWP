module.exports = function(grunt) {
	grunt.initConfig({
		nugetrestore: {
			restore: {
				src: 'openhabUWP.UI/packages.config',
				dest: 'packages/'
			}
		},
		msbuild: {
			dev: {
				src: ['openhabUWP.sln'],
				options: {
					projectConfiguration: 'Release',
					platform: 'x86',
					targets: ['Clean', 'Rebuild'],
					version: 14.0,
					maxCpuCount: 4,
					buildParameters: {
						WarningLevel: 2
					},
					verbosity: 'normal'
				}
			}
		}
	});
	grunt.loadNpmTasks('grunt-msbuild');
	grunt.loadNpmTasks('grunt-nuget');
	grunt.registerTask('default', ['nugetrestore', 'msbuild']);
};