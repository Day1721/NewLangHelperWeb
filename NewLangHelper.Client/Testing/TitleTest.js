describe('NLH title test', () => {
    it('Should have a propper title', () => {
        browser.get('http://localhost:16254/');

        expect(browser.getTitle()).toEqual('New Lang Helper');
    });
});