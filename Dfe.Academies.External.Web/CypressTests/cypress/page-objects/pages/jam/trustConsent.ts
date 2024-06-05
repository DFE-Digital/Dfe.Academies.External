import 'cypress-file-upload'

class TrustConsent {
  public JAMTrustConsentElementsVisible(): this {
    cy.get('a[class=govuk-back-link]').contains('Back')
    cy.get('.govuk-caption-l').contains('PLYMOUTH CAST (step 1 of 3)')
    cy.get('.govuk-heading-l').contains('Trust consent')
    cy.get('legend').eq(0).contains('Upload evidence that the trust consents to the school joining')
    cy.get('.govuk-label').eq(0).contains('This can be either a letter of consent from the trust, or the minutes of their board meeting.')
    cy.get('.govuk-label').eq(1).contains('Upload a file')
    cy.get('#trustConsentFileUpload').should('be.visible')
    cy.get('legend').eq(1).contains('Uploaded files')
    cy.get('hr').eq(0).should('be.visible')
    cy.get('p').eq(3).contains('No file uploaded')
    cy.get('hr').eq(1).should('be.visible')
    cy.get('input[type=submit]').should('be.visible').contains('Save and continue')

    return this
  }

  public JAMTrustConsentFileUploadAndSubmit(): this {
    const filepath = '../fixtures/fifty-k.docx'
    cy.get('#trustConsentFileUpload').attachFile(filepath)
    cy.get('input[type=submit]').click()

    return this
  }
}

const trustConsent = new TrustConsent()

export default trustConsent
