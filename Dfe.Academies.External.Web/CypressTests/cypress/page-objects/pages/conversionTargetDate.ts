class ConversionTargetDate {
  public enterConversionTargetDate(choice: string): this {
    const selector = `#selectoption${choice === 'Yes' ? 'Yes' : 'No'}`
    cy.get(selector).click()
    cy.get(selector).should('be.checked')

    if (choice === 'Yes') {
      cy.get('[id=sip_ctddiferentdatevalue-day]').type('3')
      cy.get('[id=sip_ctddiferentdatevalue-month]').type('3')
      cy.get('[id=sip_ctddiferentdatevalue-year]').type('2023')

      cy.get('[id=TargetDateExplained]').type('Conversion date explanation')
    }

    cy.get('input[type=submit]').click()

    return this
  }
}

const conversionTargetDate = new ConversionTargetDate()

export default conversionTargetDate
