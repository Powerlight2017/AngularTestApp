const SpecReporter = require('karma-spec-reporter').SpecReporter;

module.exports = function (config) {
  config.set({
    basePath: '',
    frameworks: ['jasmine', '@angular-devkit/build-angular'],
    plugins: [
      require('karma-jasmine'),
      require('karma-chrome-launcher'),
      require('karma-jasmine-html-reporter'),
      require('karma-coverage'),
      require('karma-spec-reporter'),
      require('@angular-devkit/build-angular/plugins/karma')
    ],
    specReporter: {
      maxLogLines: 5,        
      suppressErrorSummary: false,  
      suppressFailed: false,  
      suppressPassed: false, 
      suppressSkipped: true,
      showSpecTiming: true
    },
    client: {
      clearContext: false // leave Jasmine Spec Runner output visible in browser
    },
    coverageReporter: {
      dir: require('path').join(__dirname, './src'),
      subdir: '.',
      reporters: [
        { type: 'html' },
        { type: 'text-summary' }
      ]
    },
    reporters: ['spec'],
    port: 9876,
    colors: true,
    logLevel: config.LOG_INFO,
    autoWatch: true,
    browsers: ['ChromeHeadless'],
    customLaunchers: {
      ChromeHeadless: {
        base: 'Chrome',
        flags: ['--disable-gpu', '--remote-debugging-port=9222', '--no-sandbox']
      }
    },
    singleRun: true,
    restartOnFileChange: true
  });
}