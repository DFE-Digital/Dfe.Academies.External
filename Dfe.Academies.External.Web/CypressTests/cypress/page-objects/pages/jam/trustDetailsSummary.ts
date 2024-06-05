class TrustDetailsSummary {
  public JAMTrustDetailsSummaryElementsVisible(applicationId: string): this {
    cy.get('.govuk-back-link').contains('Back')
    cy.get('.govuk-caption-l').contains('Join a multi-academy trust')
    cy.get('.govuk-heading-l').contains('PLYMOUTH CAST')
    cy.get('.govuk-heading-m').eq(0).contains('The trust the school is joining')
    cy.get('.govuk-body').contains('The name of the trust')
    cy.get(`a[href="/trust/join-amat/application-select-trust?appId=${applicationId}&urn=0"]`).contains('Change your answers')
    cy.get('.govuk-heading-m').eq(1).contains('Details')
    cy.get('p').eq(3).contains('Upload evidence that the trust consents to the school joining')
    cy.get('p').eq(5).contains('Will there be any changes to the governance of the trust due to the school joining?')
    cy.get('p').eq(7).contains('Will there be any changes at a local level due to this school joining?')
    cy.get('p').eq(8).contains('Not Entered')
    cy.get('a[class="govuk-button govuk-button--secondary"]').should('be.visible').contains('Start section')
    cy.get('a[class=govuk-button]').should('be.visible').contains('Save and return to your application')

    return this
  }

  public JAMTrustDetailsSummarySelectStartSection(): this {
    cy.get('a[class="govuk-button govuk-button--secondary"]').click()

    return this
  }

  public JAMTrustDetailsSummarySaveAndReturnToApp(applicationId: string): this {
    cy.get(`a[href="/application-overview?appId=${applicationId}"]`).eq(1).click()

    return this
  }
}

const trustDetailsSummary = new TrustDetailsSummary()

export default trustDetailsSummary
