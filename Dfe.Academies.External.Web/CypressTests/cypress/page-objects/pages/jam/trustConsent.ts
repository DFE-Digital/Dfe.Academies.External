import 'cypress-file-upload'

class TrustConsent {
  public JAMTrustConsentFileUploadAndSubmit(): this {
    const filepath = '../fixtures/fifty-k.docx'
    cy.get('#trustConsentFileUpload').attachFile(filepath)
    cy.get('input[type=submit]').click()

    return this
  }
}

const trustConsent = new TrustConsent()

export default trustConsent
