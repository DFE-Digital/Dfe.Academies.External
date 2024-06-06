class ConversionTargetDate {
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
