# PathfinderAI

## Introduction

PathfinderAI is a comprehensive application and DLL designed to empower users in making informed decisions within the dynamic realm of financial markets, with a specific focus on cryptocurrency trading. By harnessing the power of machine learning, PathfinderAI provides predictive analysis through multiple linear regression, polynomial regression, and logistic regression models.

## Help
For help join the discord [here](https://discord.gg/QuCku5yyfu) or read the documentation [here](https://pathfinderai.000webhostapp.com/doc.html).

## Purpose

The primary purposes of PathfinderAI include:

1. **Price Prediction:** Utilize multiple linear regression to predict future price movements based on historical data.

2. **Trend Analysis:** Leverage polynomial regression for in-depth trend analysis and forecasting.

3. **Logistic Regression:** Implement logistic regression for predicting price trends, aiding in decision-making.

4. **Real-Time Data Retrieval:** Access real-time cryptocurrency prices through the application.

## Key Features

### 1. Multiple Linear Regression (MLR)

- **Class**: `PathfinderAI`
- **Method**: `PathfinderAI.Predict()`
- **Description**: Performs multiple linear regression on the provided data to predict future price movements.

### 2. Polynomial Regression

- **Class**: `PathfinderAI_PolynomialRegression`
- **Method**: `PathfinderAI_PolynomialRegression.Predict()`
- **Description**: Utilizes polynomial regression on historical data to make predictions about future price trends.

### 3. Logistic Regression

- **Class**: `PathfinderAI_LogisticRegression`
- **Method**: `PathfinderAI_LogisticRegression.Run()`
- **Description**: Implements logistic regression to predict price trends based on historical data.

## DLL Parameters
The DLL exposes the following parameters:

| Name                            | Type   | Description                                           | Required Arguments | Arg Type    |
|---------------------------------|--------|-------------------------------------------------------|--------------------|-------------|
| `PathfinderAI_PolynomialRegression`  | class  | Perform an action on polynomial regression data      | NaN                | NaN         |
| `PathfinderAI_PolynomialRegression.Predict()` | string | Perform polynomial regression on data            | 1                  | string(url) |
| `PathfinderAI`                   | class  | Perform an action on data with a basic MLR model     | NaN                | NaN         |
| `PathfinderAI.Predict()`         | string | Perform MLR regression on data                      | 1                  | string(url) |
| `PathfinderAI.GetCurrentBitcoinPrice()` | string | Retrieve the current price of the target crypto/currency | 1                  | string(url) |
| `PathfinderAI.lasterror`         | variable string | Display a sum of errors before the last predictions | NaN                | NaN         |
| `PathfinderAI_LogisticRegression` | class  | Perform an action on data with a basic logistic model | NaN                | NaN         |
| `PathfinderAI_LogisticRegression.Run()` | string | Perform logistic regression on data                | 1                  | string(url) |
| `PathfinderAI_LogisticRegression.strlogic` | variable string | Retrieves all the last results of the logistic regression model in a string | NaN                | NaN         |


### Thank you for using PathfinderAI ❤️





