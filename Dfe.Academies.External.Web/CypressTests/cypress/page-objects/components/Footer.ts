import BasePage from "../BasePage"
export default class Footer extends BasePage {

    static checkFooterLinksVisible()
    {
        cy.checkFooterLinksVisible()
    }

    static accessibilityStatementLinkVisible():void {
        cy.get('a[href="/accessibility-statement"]').should('be.visible').contains('Accessibility statement')
    }

    static cookiesLinkVisible():void {
        cy.contains('Cookie policy').should('be.visible').contains('Cookie policy')
    }

    static termsAndConditionsLinkVisible():void {
        cy.get('a[href="/terms"]').should('be.visible').contains('Terms and Conditions')
    }

    static privacyLinkVisible():void {
        cy.get('a[href="/privacy"]').should('be.visible').contains('Privacy')
    }

    static oglLogoVisible():void {
        cy.get('.govuk-footer__licence-logo').should('be.visible')
    }

    static allContentTextVisible():void {
        cy.get('.govuk-footer__licence-description').should('be.visible').contains('All content is available under the Open Government Licence v3.0, except where otherwise stated')
    }

    static openGovernmentLicence3LinkVisible():void {
        cy.get('a[href="https://www.nationalarchives.gov.uk/doc/open-government-licence/version/3/"]').should('be.visible').contains('Open Government Licence v3.0')
    }

    static crownCopyrightLinkVisible():void {
        cy.get('.govuk-footer__link.govuk-footer__copyright-logo').should('be.visible').contains('© Crown copyright')
    }



}