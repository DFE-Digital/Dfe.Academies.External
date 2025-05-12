import login from "../page-objects/pages/login";
import home from '../page-objects/pages/home'

// cypress/e2e/saml_login.cy.ts
describe('Test user can sign-in', () => {
	beforeEach(function () {
    cy.visit(Cypress.env('URL'))
		home.start()
	})

  it('should log in to DFE Sign-In and obtain session cookies', () => {
    const baseUrl = Cypress.env('URL')

    login.login()

    //  Assert redirect back to application
    cy.url().should('include', baseUrl);

    // After successful login, get the cookies
    cy.getCookies().then((cookies) => {
      const sessionCookies = cookies.map(cookie => `${cookie.name}=${cookie.value}`).join(';');
      // Check if we are in a CI environment before writing the file
      if (Cypress.env('CI')) {
        cy.writeFile('cypress/fixtures/session_cookies.json', { cookies: sessionCookies });
      } else {
        cy.log("Not in CI environment, skipping cookie file creation.");
      }
    });

  });
});
