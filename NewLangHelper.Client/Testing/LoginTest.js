describe('NLH login',() => {
    it('should log in',() => {
        browser.get('http://localhost:16254 /');

        browser.waitForAngular();

        let navRight = element(by.tagName('ng-include'))
            .element(by.tagName('ul'))
            .all(by.tagName('li'));

        navRight.get(0)
            .element(by.tagName('p'))
            .isPresent()
            .then(res => {
                if (res) {
                    navRight.get(1)
                        .element(by.tagName('a'))
                        .click();
                }
                //Now I'm sure I'm logged out
                navRight.get(0)
                    .element(by.tagName('a'))
                    .click();

                browser.waitForAngular();

                expect(browser.getCurrentUrl()).toMatch(/\/#\/login/i);

                element(by.model('username')).sendKeys('Marek');
                element(by.model('password')).sendKeys('Password1!');

                element(by.tagName('form'))
                    .all(by.tagName('input'))
                    .last().click()
                    .then(() => {
                    //Check if redirect to /home
                    browser.waitForAngular();
                    expect(browser.getCurrentUrl()).toMatch(/\/#\/home/i);
                }, 10000);
            });
    });
});