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
					targets: ['Clean', 'Rebuild'],
					version: 14.0,
					maxCpuCount: 2,
					buildParameters: {
						WarningLevel: 2
					},
					verbosity: 'quiet',
					AppxBundle: 'Always',
					AppxBundlePlatforms: 'x86|x64|ARM'
				}
			}
		}
	});
	grunt.loadNpmTasks('grunt-msbuild');
	grunt.loadNpmTasks('grunt-nuget');
	grunt.registerTask('default', ['nugetrestore', 'msbuild']);
};