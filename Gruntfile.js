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
					platform: 'ARM',
					version: 14.0,
					maxCpuCount: 2,
					buildParameters: {
						WarningLevel: 2
					},
					verbosity: 'quiet',
					AppxBundle: 'Always',
					AppxBundlePlatforms: 'x86|x64|ARM'
				}
			},
			x86: {
				src: ['openhabUWP.sln'],
				options: {
					projectConfiguration: 'Release',
					targets: ['Clean', 'Rebuild'],
					platform: 'x86',
					version: 14.0,
					maxCpuCount: 2,
					buildParameters: {
						WarningLevel: 2
					},
					verbosity: 'quiet',
					AppxBundle: 'Always',
					AppxBundlePlatforms: 'x86|x64|ARM'
				}
			},
			arm: {
				src: ['openhabUWP.sln'],
				options: {
					projectConfiguration: 'Release',
					targets: ['Clean', 'Rebuild'],
					platform: 'ARM',
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
	grunt.registerTask('default', ['nugetrestore', 'msbuild:arm', 'msbuild:x86']);
};