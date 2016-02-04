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
					AppxBundlePlatforms: 'ARM'
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
					AppxBundlePlatforms: 'x86',
					OutputPath: 'deploy/'
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
					AppxBundlePlatforms: 'ARM',
					OutputPath: 'deploy/',
					OutDir: 'deploy/',
				}
			}
		},
		compress: {
			arm: {
				options: {
					archive: './openhabUWP-arm.zip',
					mode: 'zip'
				},
				files: [{ src: 'openhabUWP.UI/AppPackages/*ARM*/**/*.*' }]
			},
			x86: {
				options: {
					archive: './openhabUWP-arm.zip',
					mode: 'zip'
				},
				files: [{ src: 'openhabUWP.UI/AppPackages/*x86*/**/*.*' }]
			}
		}
	});
	
	grunt.loadNpmTasks('grunt-contrib-compress');
	grunt.loadNpmTasks('grunt-msbuild');
	grunt.loadNpmTasks('grunt-nuget');
	
	grunt.registerTask('default', ['nugetrestore', 'msbuild:arm', 'msbuild:x86']);
};