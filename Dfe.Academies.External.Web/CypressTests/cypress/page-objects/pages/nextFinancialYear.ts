class NextFinancialYear {
  public nextFinancialYrElementsVisible(): this {
    cy.nextFinancialYrElementsVisible()

    return this
  }

  public inputNextFinancialYrDataAndSubmit(): this {
    cy.inputNextFinancialYrDate()

    cy.inputNextFinancialYrRevenueCarryForward()

    cy.get('#revenueTypeDeficit').click()

    cy.verifyNextRevenueCarryForwardDeficitSelectedSectionDisplays()

    cy.inputReasonsForNextRevenueCarryForwardDeficit()

    cy.uploadFileForNextRevenueCarryForwardDeficit()

    cy.inputNextFinancialYrCapitalCarryForward()

    cy.selectNextCapitalCarryForwardDeficit()

    cy.verifyNextCapitalCarryForwardDeficitSelectedSectionDisplays()

    cy.inputReasonsForNextCapitalCarryForwardDeficit()

    cy.uploadFileForNextCapitalCarryForwardDeficit()

    cy.submitNextFinancialYr()

    return this
  }
}

const nextFinancialYear = new NextFinancialYear()

export default nextFinancialYear
