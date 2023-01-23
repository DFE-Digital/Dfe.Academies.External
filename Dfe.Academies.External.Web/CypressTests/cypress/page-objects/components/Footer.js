import BasePage from "../BasePage"
export default class Footer extends BasePage {

    static accessibilityStatementLinkVisible() {
        cy.get('a[href="/accessibility"]').should('be.visible').contains('Accessibility statement')
    }

    static cookiesLinkVisible() {
        cy.get('a[href="/cookies"]').should('be.visible').contains('Cookie policy')
    }

    static termsAndConditionsLinkVisible() {
        cy.get('a[href="/terms"]').should('be.visible').contains('Terms and Conditions')
    }

    static privacyLinkVisible() {
        cy.get('a[href="/privacy"]').should('be.visible').contains('Privacy')
    }

    static oglLogoVisible() {
        cy.get('.govuk-footer__licence-logo').should('be.visible')
    }

    static allContentTextVisible() {
        cy.get('.govuk-footer__licence-description').should('be.visible').contains('All content is available under the Open Government Licence v3.0, except where otherwise stated')
    }

    static openGovernmentLicence3LinkVisible() {
        cy.get('a[href="https://www.nationalarchives.gov.uk/doc/open-government-licence/version/3/"]').should('be.visible').contains('Open Government Licence v3.0')
    }

    static crownCopyrightLinkVisible() {
        cy.get('.govuk-footer__link.govuk-footer__copyright-logo').should('be.visible').contains('© Crown copyright')
    }



}