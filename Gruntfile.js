module.exports = function(grunt) {
	grunt.initConfig({
		nugetrestore: {
			restore: {
				src: 'openhabUWP.UI/packages.config',
				dest: 'packages/'
			}
		},
		msbuild: {
			x64: {
				src: ['openhabUWP.sln'],
				options: {
					projectConfiguration: 'Release',
					targets: ['Clean', 'Rebuild'],
					platform: 'x64',
					version: 14.0,
					maxCpuCount: 2,
					buildParameters: {
						WarningLevel: 2
					},
					verbosity: 'quiet',
					AppxBundle: 'Always',
					AppxBundlePlatforms: 'x64'
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
				}
			}
		},
		compress: {
			x64: {
				options: {
					archive: './openhabUWP-x64.zip',
					mode: 'zip'
				},
				files: [{ src: 'openhabUWP.UI/AppPackages/*x64*/**/*.*' }]
			},
			x86: {
				options: {
					archive: './openhabUWP-x86.zip',
					mode: 'zip'
				},
				files: [{ src: 'openhabUWP.UI/AppPackages/*x86*/**/*.*' }]
			},			
			arm: {
				options: {
					archive: './openhabUWP-ARM.zip',
					mode: 'zip'
				},
				files: [{ src: 'openhabUWP.UI/AppPackages/*ARM*/**/*.*' }]
			},
		}
	});
	
	grunt.loadNpmTasks('grunt-contrib-compress');
	grunt.loadNpmTasks('grunt-msbuild');
	grunt.loadNpmTasks('grunt-nuget');
	
	grunt.registerTask('_before', ['nugetrestore']);
	
	grunt.registerTask('_x64', ['msbuild:x64', 'compress:x64']);
	grunt.registerTask('_x86', ['msbuild:x86', 'compress:x86']);
	grunt.registerTask('_arm', ['msbuild:arm', 'compress:arm']);
	
	grunt.registerTask('default', ['_before', '_x64', '_x86', '_arm']);
};