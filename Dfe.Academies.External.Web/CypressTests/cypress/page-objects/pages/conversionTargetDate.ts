class ConversionTargetDate {
  public conversionTargetDateElementsVisible(): this {
    cy.get('.govuk-back-link').contains('Back')
    cy.get('.govuk-caption-l').contains('Conversion key details')
    cy.get('.govuk-heading-l').contains('Conversion target date')
    cy.get('.govuk-body-l').contains('Conversion usually takes around 6 months. It may take longer if the school is part of a private finance initiative (PFI) contract.')
    cy.get('.govuk-heading-s').contains('Do you want the conversion to happen on a particular date?')
    cy.get('#selectoptionYes').should('not.be.checked')
    cy.get('label[for=selectoptionYes]').contains('Yes')
    cy.get('#selectoptionNo').should('not.be.checked')
    cy.get('label[for=selectoptionNo]').contains('No')
    cy.get('input[type=submit]').should('be.visible').contains('Save and continue')

    return this
  }

  public selectConversionTargetDateOptionNo(): this {
    cy.get('#selectoptionNo').click()
    cy.get('#selectoptionNo').should('be.checked')

    return this
  }

  public conversionTargetDateSubmit(): this {
    cy.get('input[type=submit]').click()

    return this
  }
}

const conversionTargetDate = new ConversionTargetDate()

export default conversionTargetDate
