class YouDontHavePermissionsPage {
  public youDontHavePermissionsElementsVisible(): this {
    cy.origin('https://s184t01-a2bextcdnendpoint-ezfhhdbpembpanh5.z01.azurefd.net/application-access-exception', () => {
      cy.get('.govuk-back-link').contains('Back')
      cy.get('h1').contains('You do not have permission to view this application')
      cy.get('#main-content > div > div > p:nth-child(2)').contains('To view and contribute:')
      cy.get('li').eq(0).contains('you must have been invited by an existing contributor')
      cy.get('li').eq(1).contains('your email address must match the one entered for you by the person who sent you the invite')
      cy.get('#main-content > div > div > p:nth-child(4)').contains('If you have checked these details and are still seeing this message, contact regionalservices.rg@education.gov.uk')
      cy.get('a[href=mailto:regionalservices.rg@education.gov.uk]').contains('regionalservices.rg@education.gov.uk')

      return this
    })
  }
}

const youDontHavePermissionsPage = new YouDontHavePermissionsPage()

export default youDontHavePermissionsPage
