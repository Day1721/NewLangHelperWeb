# About

Implementation of NewLangHelper API, based on AngularJS routing and Bootstrap

# Before launch

Refresh packages (requires npm - [node.js](https://nodejs.org/en/) or using [apt-get](https://linux.die.net/man/8/apt-get)/[choco](https://chocolatey.org/))

```shell
npm install
```

# Launch

using python http.client (requires python 3)

```shell
python -m http.client
```

# Tests

requirements: protractor
```shell
npm install -g protractor # -g need root/admin permissions
webdriver-manager update
webdriver-manager start
```
execution (from root of project):
```shell
protractor Testing/Config.js
```
