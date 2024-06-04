class AboutTheConversion {
  public aboutTheConversionNotStartedElementsVisible(): this {
    cy.aboutTheConversionNotStartedElementsVisible()

    return this
  }

  public selectContactDetailsStartSection(): this {
    cy.get('a[class=govuk-button govuk-button--secondary]').eq(0).click()

    return this
  }

  public aboutTheConversionMainContactsCompleteElementsVisible(): this {
    cy.aboutTheConversionMainContactsCompleteElementsVisible()

    return this
  }

  public selectDateForConversionStartSection(): this {
    cy.selectDateForConversionStartSection()

    return this
  }

  public aboutTheConversionCompleteElementsVisible(): this {
    cy.aboutTheConversionCompleteElementsVisible()

    return this
  }

  public submitAboutTheConversion(): this {
    cy.get('.govuk-button').click()

    return this
  }
}

const aboutTheConversion = new AboutTheConversion()

export default aboutTheConversion
