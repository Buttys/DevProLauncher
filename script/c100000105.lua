--アルカナフォースＶＩＩＩ－ＳＴＲＥＮＧＴＨ
function c100000105.initial_effect(c)
	--coin
	local e1=Effect.CreateEffect(c)
	e1:SetDescription(aux.Stringid(100000105,0))
	e1:SetCategory(CATEGORY_COIN)
	e1:SetType(EFFECT_TYPE_SINGLE+EFFECT_TYPE_TRIGGER_F)
	e1:SetCode(EVENT_SUMMON_SUCCESS)
	e1:SetTarget(c100000105.cointg)
	e1:SetOperation(c100000105.coinop)
	c:RegisterEffect(e1)
	local e2=e1:Clone()
	e2:SetCode(EVENT_SPSUMMON_SUCCESS)
	c:RegisterEffect(e2)
	local e3=e1:Clone()
	e3:SetCode(EVENT_FLIP_SUMMON_SUCCESS)
	c:RegisterEffect(e3)
end
function c100000105.cointg(e,tp,eg,ep,ev,re,r,rp,chk)
	if chk==0 then return true end
	Duel.SetOperationInfo(0,CATEGORY_COIN,nil,0,tp,1)
end
function c100000105.coinop(e,tp,eg,ep,ev,re,r,rp)
	local c=e:GetHandler()
	if not c:IsRelateToEffect(e) or c:IsFacedown() then return end
	local res=0
	if c:IsHasEffect(73206827) then
		res=1-Duel.SelectOption(tp,60,61)
	else res=Duel.TossCoin(tp,1) end
	if res==0 then
		if Duel.IsExistingTarget(Card.IsControlerCanBeChanged,tp,LOCATION_MZONE,0,1,nil) then 
			Duel.Hint(HINT_SELECTMSG,1-tp,HINTMSG_CONTROL)			
			local g=Duel.SelectTarget(1-tp,Card.IsControlerCanBeChanged,1-tp,0,LOCATION_MZONE,1,1,e:GetHandler())
			Duel.HintSelection(g)
			Duel.BreakEffect()
			if g:GetFirst() then
				Duel.GetControl(g:GetFirst(),1-tp)
			end
		end
	else
		if Duel.IsExistingTarget(Card.IsControlerCanBeChanged,tp,0,LOCATION_MZONE,1,nil) then 
			Duel.Hint(HINT_SELECTMSG,tp,HINTMSG_CONTROL)			
			local g=Duel.SelectTarget(tp,Card.IsControlerCanBeChanged,tp,0,LOCATION_MZONE,1,1,nil)			
			Duel.HintSelection(g)
			Duel.BreakEffect()
			if g:GetFirst() then
				Duel.GetControl(g:GetFirst(),tp)
			end
		end
	end
end
