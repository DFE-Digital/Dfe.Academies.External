class Footer {
  public checkFooterLinksVisible(): this {
    cy.get('a[href="/accessibility-statement"]').should('be.visible').contains('Accessibility statement')
    cy.contains('Cookie policy').should('be.visible').contains('Cookie policy')
    cy.get('a[href="/terms"]').should('be.visible').contains('Terms and Conditions')

    cy.get('a[href="/privacy"]').should('be.visible').contains('Privacy')
    cy.get('.govuk-footer__licence-logo').should('be.visible')
    cy.get('.govuk-footer__licence-description').should('be.visible').contains('All content is available under the Open Government Licence v3.0, except where otherwise stated')
    cy.get('a[href="https://www.nationalarchives.gov.uk/doc/open-government-licence/version/3/"]').should('be.visible').contains('Open Government Licence v3.0')
    cy.get('.govuk-footer__link.govuk-footer__copyright-logo').should('be.visible').contains('© Crown copyright')

    return this
  }
}

const footer = new Footer()

export default footer
