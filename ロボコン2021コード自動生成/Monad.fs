module ロボコン2021コード自動生成.Monad


type Either<'T, 'U> =
    | Left of 'T
    | Right of 'U

let ret x = Right x

let (>>=) (m) f =
    match m with
    | Right x -> f x
    | Left _ -> m

type EitherBuilder() =
    member _.Return(x) = ret x
    member _.Bind(m, f) = m >>= f

let either = EitherBuilder()
